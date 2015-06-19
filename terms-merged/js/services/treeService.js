angular.module('TermsDataEntry')
.factory('treeService', function () {
    return {
        getTree: function getTree($el) {
            return $el.parents('[data-type="tree-root"]')
        },

        getTreeId: function getTreeId($tree, $el) {
            return $tree.attr('id').replace('tree-root_', '') + ':' + $el.data('id')
        },

        /**
         * Get the node corresponding to the treeId and node id
         * @param id
         * @param tabsetId
         * @returns {*|jQuery}
         */
        getNode: function getNode(id, tabsetId) {
            var ids = id.split(':'),
                tree = this.getTreeFromId(id, tabsetId),
                node = tree.find('[data-id="' + ids[1] + '"]')
            return node;
        },

        getTreeFromId: function getTreeFromId(id, tabsetId) {
            var ids = id.split(':'),
                tree = $('#' + tabsetId + '_tabset').find('#tree-root_' + ids[0]);
            return tree;
        }
    }
});