(function () {
    angular.module('TermsDataEntry')
     .controller("MainController", MainController);
    function MainController($scope, $modal, $location, $q, $compile, linkService, treeService, treeServer, loginService, connectionServer, nodeServer) {

        if (!loginService.getUser()) {
            $location.path('/login')
        }
        else {
            linkService.container = $('.main-view');
            this.tabsets = [{
                id: 'left'
            }, {
                id: 'right'
            }];

            $scope.deleteConnection = function (id) {
                connectionServer.deleteConnection(id).then(function () {
                    linkService.removeLink(id);
                    $scope.redrawLines();
                    $scope.resetConnecting();
                });

            };
            $scope.treeServer = treeServer;

            treeServer.getTrees().then(function (trees) {
                $scope.trees = trees;
                return treeServer.getTree($scope.trees[0].id);
            }).then(function (tree) {
                $scope.trees[0].list = [tree];

                $scope._leftTree = $scope._rightTree = $scope.trees[0].id;


                var remove = function (scope) {
                    // deleting the node in the server first, then (if success) delete it in UI
                    nodeServer.deleteNode($scope.xmlFileId,
                                          // node id
                                          scope.$nodeScope.$modelValue.id,
                                          // parent node id (0 if it doesn't has parent)
                                          scope.$nodeScope.$parentNodeScope ? scope.$nodeScope.$parentNodeScope.$modelValue.id : 0).then(function () {

                                              scope.remove(); // calling the node's ui-tree function to remove it;

                                          }, function (e) {
                                              // if something happened, it is handled here
                                          });
                };

                $scope.toggle = function (scope) {
                    scope.toggle();
                };

                var newSubItem = function (scope) {
                    var nodeData = scope.$nodeScope.$modelValue, //getting the object which the node represents
                        parentId = scope.$nodeScope.$modelValue.id,
                        treeId = scope.tree.id,
                        newNode = {};

                    var modalInstance = $modal.open({
                        templateUrl: 'templates/partials/add_node_modal.html',
                        controller: 'AddNodeModalCtrl'
                    });

                    modalInstance.result.then(function (data) {
                        // If user save the info
                        newNode.title = data.DESCRIPTOR;
                        newNode.properties = data;


                        // node is added in the server first, then (if success) is added in UI
                        nodeServer.addNode(treeId,
                                           data.DESCRIPTOR,
                                           // parent id (if it has one)
                                           parentId,

                                           data).then(function (newId) {
                                               //setting the id which came from server
                                               newNode.id = newId;

                                               // adding a new item
                                               nodeData.items.push(newNode);
                                           }, function (e) {
                                               // if something happened, it is handled here
                                           });
                    }, function () {
                        // User cancel the node adding
                    });
                };

                var showDetails = function showDetails(scope) {

                    var modalInstance = $modal.open({
                        templateUrl: 'templates/partials/term_detail_modal.html',
                        controller: 'TermDetailsModalCtrl',
                        size: "lg",
                        resolve: {
                            term: function () {
                                return scope.$nodeScope.$modelValue;
                            }
                        }
                    });

                };

                $scope.contextmenu = [{
                    text: "Details", // Text to displat the menu option
                    action: function (scope) { // action to execute when clicked
                        showDetails(scope); // calls the newSubItem function
                    }
                }, {
                    text: "Add a Term", // Text to displat the menu option
                    action: function (scope) { // action to execute when clicked
                        newSubItem(scope); // calls the newSubItem function
                    }
                }, {
                    text: "Delete Term",
                    action: function (scope) {
                        remove(scope); // calls the remove function
                    }
                }];

                /**
                 * Connect nodes from different tabsets
                 */
                $scope.connect = function (scope) {
                    var $el = scope.$element;
                    $el.toggleClass('selected');

                    var parentId = $scope.getTabsetId($el);
                    $scope[parentId] = $el.hasClass('selected') ? $el : null; // remember the selected element with respect to the tree containing the element

                    var left = $scope['_left'],
                        right = $scope['_right'];

                    if (left && right) { // if there are selected items in both trees
                        var leftTree = treeService.getTree(left),
                            rightTree = treeService.getTree(right),
                            leftId = treeService.getTreeId(leftTree, left),
                            rightId = treeService.getTreeId(rightTree, right);

                        if (!linkService.findLink(leftId, rightId)) { // and there is no link between the items

                            var modalInstance = $modal.open({
                                templateUrl: 'templates/partials/connection.modal.html',
                                controller: 'ConnectionModalCtrl',
                                scope: $scope,
                                resolve: {
                                    data: function () {
                                        return {
                                            term1: left,
                                            term2: right,
                                            leftId: leftId,
                                            rightId: rightId
                                        };
                                    }
                                }
                            });

                            modalInstance.result.then(function (data) {

                                data.Connection_Left_Term_Id = left.scope().$nodeScope.$modelValue.id;
                                data.Connection_Right_Term_Id = right.scope().$nodeScope.$modelValue.id;

                                connectionServer.addConnection(data).then(function (newConnection) {
                                    data = newConnection;
                                    link = {
                                        first: left,
                                        firstId: leftId,
                                        second: right,
                                        secondId: rightId,
                                        connectionId: data.Connection_Id,
                                        data: data,
                                        user_id: data.Connection_Id_User
                                    };
                                    linkService.addLink(link);

                                    var line = linkService.drawLine(left, right, data.Connection_Id, false, link.user_id == loginService.getUser().User_Id); // and draw
                                    linkService.addTooltipToLink(line, link);
                                    $compile(line)($scope);

                                    buildContextMenu(link);

                                    $scope.resetConnecting(); // and clear state
                                });
                            });
                        }
                    }
                    return true;
                };
                function buildContextMenu(link) {

                    var context = $('<div class="dropdown" style="position: fixed;z-index:999;cursor:pointer;" id="menu-' + link.connectionId + '"><ul class="dropdown-menu" role="menu"><li><a class="pointer" ng-click="deleteConnection(' + link.connectionId + ')" role="menuitem" tabindex="1">Delete Connection </a></li></ul></div> ');
                    context.appendTo('body');
                    $compile(context)($scope);
                }
                /**
                 * Recursive function which gets the conectios form the nodes
                 */
                function getConnectionsForNodes(nodes, tree) {
                    if (nodes.length) {
                        return $scope.downloadConnectionsForNodes(nodes.slice(0, 5), tree).then(function () {
                            return getConnectionsForNodes(nodes.slice(5), tree);
                        });
                    }

                }

                function getConnectionForNodesChildren(nodes) {
                    return getConnectionsForNodes(nodes.reduce(function (beg, itm) {
                        return beg.concat(itm.items);
                    }, []));
                }

                /**
                 * Download the connections for the tree
                 */
                $scope.downloadConnections = function downloadConnections(tree) {
                    return getConnectionsForNodes(tree.list, tree);
                };

                /**
                 * Download the connections for the nodes and it childs
                 */
                $scope.downloadConnectionsForNodes = function downloadConnectionsForNodes(nodes, tree) {
                    return $q(function (resolve, reject) {
                        connectionServer.getConnectionsForTerms(nodes.map(function (item) { return item.id })).then(function (connections) {
                            connections.forEach(function (item) {
                                linkService.addLink({
                                    firstId: item.Connection_Left_Tree_Id + ":" + item.Connection_Left_Term_Id,
                                    secondId: item.Connection_Right_Tree_Id + ":" + item.Connection_Right_Term_Id,
                                    connectionId: item.Connection_Id,
                                    data: item,
                                    user_id: item.Connection_Id_User
                                });
                            });

                            if (connections.length) $scope.redrawLines();

                        }).then(function () {
                            return getConnectionForNodesChildren(nodes);
                        }).then(resolve, reject);
                    });
                };

                /**
                 * Clear the state if connection of nodes is aborted
                 */
                $scope.resetConnecting = function () {
                    var left = $scope['_left'];
                    var right = $scope['_right'];
                    left && left.toggleClass('selected');
                    right && right.toggleClass('selected');
                    $scope['_left'] = null;
                    $scope['_right'] = null;
                };

                /**
                 * Options of the tree which are via ui-tree attribute
                 * @type {{dragStop: Function, removed: Function}}
                 */
                $scope.treeOptions = {
                    dragStop: function () {
                        $scope.redrawLines();
                        $scope.resetConnecting();
                    },
                    removed: function (scope) {
                        var $el = scope.$element.children('.angular-ui-tree-handle:first'),
                            $tree = treeService.getTree($el);
                        linkService.removeLinks(treeService.getTreeId($tree, $el));
                        $scope.redrawLines();
                    },
                    dropped: function (event) { // when an node is dropped somewhere, notify to the server the action
                        nodeServer.moveNode($scope.xmlFileId,
                                            // dropped item id
                                            event.source.nodeScope.$modelValue.id,
                                            // previous parent id (if it had no parent sends 0)
                                            event.source.nodesScope.$nodeScope ? event.source.nodesScope.$nodeScope.$modelValue.id : null,
                                            // new parent id (if it has no new parent sends 0)
                                            event.dest.nodesScope.$nodeScope ? event.dest.nodesScope.$nodeScope.$modelValue.id : null);
                    }
                };

                /**
                 * Redraw lines after toggling visibility of child nodes
                 */
                $scope.afterToggle = function () {
                    $scope.redrawLines();
                };

                /**
                 * Remove the lines and show only relative ones
                 */
                $scope.redrawLines = function () {
                    setTimeout(function () { // async to let all the dom changes to take effect
                        $('.line').remove();
                        $('.circle').remove();
                        var l = linkService.links.length,
                            link;
                        while (l-- > 0) {
                            link = linkService.links[l];
                            if ($scope.shouldShowLink(link)) {

                                var firstEnd = treeVisible(link.firstId, '_left') ? treeService.getNode(link.firstId, 'left') : treeService.getNode(link.secondId, 'left');
                                var secondEnd = treeVisible(link.secondId, '_right') ? treeService.getNode(link.secondId, 'right') : treeService.getNode(link.firstId, 'right');

                                var line = linkService.drawLine(firstEnd, secondEnd, link.connectionId, true, link.user_id == loginService.getUser().User_Id);
                                linkService.addTooltipToLink(line, link);
                                $compile(line)($scope);
                                buildContextMenu(link);

                            }
                        }
                    })
                };

                /**
                 * Indicates if a tree is visible
                 */
                function treeVisible(id, tabset) {
                    return $scope[tabset + 'Tree'] == id.split(':')[0];
                }

                /**
                 * Find if the link nodes belong two visible trees
                 */
                $scope.shouldShowLink = function (link) {

                    return link &&
                        ((treeVisible(link.firstId, '_left') && treeVisible(link.secondId, '_right')) ||
                         (treeVisible(link.firstId, '_right') && treeVisible(link.secondId, '_left')));


                };

                /**
                 * Change active tab/tree
                 * @param index tab index
                 * @param tabsetId
                 */
                $scope.select = function (index, tabsetId) {
                    $scope['_' + tabsetId + 'Tree'] = this.tree.id;

                    if (!this.tree.list || !this.tree.list.length) {
                        var tree = this.tree;
                        treeServer.getTree(tree.id).then(function (treeInfo) {
                            tree.list = [treeInfo];

                            $scope.downloadConnections(tree).then(function () {
                                $scope.redrawLines();
                            });

                            if ($scope.$$phase !== "$digest") $scope.$digest();
                        }, console.log);
                    } else {
                        $scope.redrawLines();
                    }
                };

                $scope.getTabsetId = function ($el) {
                    return '_' + $el.parents('.tabset').attr('id').replace('_tabset', '');
                };

                /**
                 * Redraw on resize
                 */
                $(window).on('resize', function () {
                    $scope.redrawLines();
                });

                setTimeout($scope.downloadConnections.bind($scope, $scope.trees[0]), 1000);

            }, console.log);
        }
    }
}())