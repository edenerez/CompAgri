using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace CompAgri.Common
{
    public class DataHelper
    {
        internal static int AddNode(int xmlFileId, String name, int parentId, JObject param)
        {
            var term = AddTerm(xmlFileId, name);

            if (parentId != 0)
            {
                AddRelation(parentId, term);
            }

            if (param != null)
            {
                SaveTermProperties(term, param);
            }

            return term.Term_Id;
        }

        internal static void MoveNode(int nodeId, int? oldParentId, int? newParentId)
        {
            using (CompAgriConnection ctx = new CompAgriConnection())
            {
                var relation = ctx.Relations.Where(o => o.Relation_Parent_Term_Id == oldParentId && o.Relation_Child_Term_Id == nodeId)
                    .FirstOrDefault();

                if (relation != null)
                {
                    relation.Relation_Parent_Term_Id = newParentId;
                }
                else
                {
                    relation = new Relation
                    {
                        Relation_Child_Term_Id = nodeId,
                        Relation_Parent_Term_Id = newParentId
                    };

                    ctx.Relations.Add(relation);
                }

                ctx.SaveChanges();
            }
        }

        internal static void DeleteNode(int nodeId, int parentId)
        {
            using (CompAgriConnection ctx = new CompAgriConnection())
            {
                var relationToDelete = ctx.Relations.Where(o => o.Relation_Parent_Term_Id == parentId && o.Relation_Child_Term_Id == nodeId)
                    .FirstOrDefault();

                if (relationToDelete != null)
                {
                    ctx.Relations.Remove(relationToDelete);
                    ctx.SaveChanges();
                }
            }
        }

        internal static void UploadFile(string filePath)
        {
            using (CompAgriConnection ctx = new CompAgriConnection())
            {
                ctx.Configuration.AutoDetectChangesEnabled = false;

                string path = filePath;
                string fileName = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                XDocument docX = XDocument.Load(path);

                XmlFile documentDB = new XmlFile();
                ctx.XmlFiles.Add(documentDB);
                documentDB.XmlFile_Name = fileName;

                ctx.SaveChanges();

                IEnumerable<XElement> concept = docX.Descendants("CONCEPT");
                foreach (XElement item in concept)
                {
                    AddConcept(documentDB, item, ctx);
                }
                ctx.SaveChanges();

            }

        }

        private static void AddConcept(XmlFile documentDB, XElement item, CompAgriConnection ctx)
        {
            Term currentTerm = null;
            foreach (XElement child in item.Descendants())
            {

                if (child.Name == "DESCRIPTOR")
                {
                    currentTerm = AddTerm(child, documentDB, ctx);
                }
                else if (child.Name == "BT")
                {
                    AddParentRelationToTerm(currentTerm, child, documentDB, ctx);
                }
                else if (child.Name == "NT")
                {
                    AddChildRelationToTerm(currentTerm, child, documentDB, ctx);
                }
                else
                {
                    AddPropertyToTerm(ctx, currentTerm, child);
                }
            }
            ctx.SaveChanges();
        }

        private static void AddPropertyToTerm(CompAgriConnection ctx, Term currentTerm, XElement child)
        {
            Property newProperty = new Property()
            {
                Property_Key = child.Name.LocalName,
                Property_Value = child.Value,
                Property_Term_Id = currentTerm.Term_Id
            };

            currentTerm.Property.Add(newProperty);
            ctx.Properties.Add(newProperty);
        }

        private static void AddChildRelationToTerm(Term currentTerm, XElement child, XmlFile documentDB, CompAgriConnection ctx)
        {
            Term childTerm = null;
            childTerm = documentDB.Term.FirstOrDefault(d => d.Term_Title == child.Value);
            if (childTerm == null)
            {
                childTerm = new Term();
                childTerm.Term_Title = child.Value;
                childTerm.Term_XmlFile_Id = documentDB.XmlFile_Id;
                ctx.Terms.Add(childTerm);
                ctx.SaveChanges();
            }

            Relation relation = new Relation()
            {
                Term = currentTerm,
                Term1 = childTerm
            };
            ctx.Relations.Add(relation);
        }

        private static void AddParentRelationToTerm(Term currentTerm, XElement parentInfo, XmlFile documentDB, CompAgriConnection ctx)
        {
            Term parentTerm = null;
            parentTerm = documentDB.Term.FirstOrDefault(d => d.Term_Title == parentInfo.Value);
            if (parentTerm == null)
            {
                parentTerm = new Term();
                parentTerm.Term_Title = parentInfo.Value;
                parentTerm.Term_XmlFile_Id = documentDB.XmlFile_Id;
                ctx.Terms.Add(parentTerm);
                ctx.SaveChanges();
            }

            Relation relation = new Relation()
            {
                Term = parentTerm,
                Term1 = currentTerm
            };
            ctx.Relations.Add(relation);
        }

        private static Term AddTerm(XElement description, XmlFile documentDB, CompAgriConnection ctx)
        {
            Term currentTerm = documentDB.Term.FirstOrDefault(d => d.Term_Title == description.Value);
            if (currentTerm == null)
            {
                currentTerm = new Term();
                currentTerm.Term_Title = description.Value;
                currentTerm.Term_XmlFile_Id = documentDB.XmlFile_Id;
                ctx.Terms.Add(currentTerm);
                ctx.SaveChanges();
            }
            return currentTerm;
        }


        internal static Models.Tree.Tree<Models.Terms.Term>.RootClass getTree(int xmlFileId)
        {
            // Get the XMLFile
            var XmlFile = Models.Terms.XMLFile.Find(xmlFileId);

            // If file not found so we return not found
            if (XmlFile == null)
                return null;

            // We Ask the tree and return the root of the tree
            return XmlFile.getXMLAsTree().Root;
        }

        internal static Models.TermDetails TermDetails(int termId)
        {
            var term = Models.Terms.Term.Find(termId);

            if (term == null)
                return null;

            return new Models.TermDetails(term);
        }

        private static Models.Terms.Term AddTerm(int xmlFileId, String name)
        {
            var term = new Models.Terms.Term
            {
                Term_XmlFile_id = xmlFileId,
                Term_Title = name
            };

            term.Save();
            return term;
        }

        private static void AddRelation(int parentId, Models.Terms.Term term)
        {
            var relation = new Models.Terms.Relation()
            {
                Relation_Parent_Term_Id = parentId,
                Relation_Child_Term_Id = term.Term_Id
            };

            relation.Save();
        }

        private static void SaveTermProperties(Models.Terms.Term term, JObject param)
        {
            var propList = new List<Models.Terms.T_Property>();

            foreach (string prop in Models.Terms.T_Property.AllowedProperties)
            {
                if (param[prop] != null)
                {
                    propList.Add(new Models.Terms.T_Property
                    {
                        Property_Term_Id = term.Term_Id,
                        Property_Key = prop,
                        Property_Value = param[prop].ToString()
                    });
                }
            }

            Models.Terms.T_Property.SaveMultiple(propList);
        }


    }
}
