angular.module('TermsDataEntry')
.factory('linkService', function () {
    return {
        links: [],

        container: $('body > .container'),
        /**
        * Add the link between items to the cache
        * @param link link
        */
        addLink: function addLink(link) {
            this.links.push(link);
            //console.log('Connecting node [' + link.firstId + '] and [' + link.secondId + ']');
        },

        /**
         * remove  link between items to the cache
         * @param link link
         */
        removeLink: function removeLink(id) {
            var newLinks = [];
            this.links.forEach(function (link) {
                if (link.connectionId != id) {
                    newLinks.push(link);
                }
            });

            this.links = angular.copy(newLinks);
        },
        /**
         * Find if there is a link between two items and return it
         * @param firstId
         * @param secondId
         * @returns {*} a link
         */
        findLink: function findLink(firstId, secondId) {
            var linksLength = this.links.length,
                link;

            while (linksLength-- > 0) {
                link = this.links[linksLength];
                if (link && link.firstId == firstId && link.secondId == secondId) {
                    return link;
                }
            }
            return null;
        },

        /**
         * Removes every link that contains the element
         * @param id
         * @returns {*}
         */
        removeLinks: function removeLinks(id) {
            var linksLength = this.links.length,
                link;
            while (linksLength-- > 0) {
                link = this.links[linksLength];
                if (link && (link.firstId == id || link.secondId == id)) {
                    this.links[linksLength] = null;
                }
            }
        },

        /**
         * Draw a line between two elements with two dots at the ends
         * @param _1 first jqLite/jquery element
         * @param _2 second jqLite/jquery element
         * @param jq - true if elements are jquery elements and false if jqLite elements
         */
        drawLine: function drawLine(_1, _2, id, jq, addContextMenu) {
            var el1 = this.getElement(_1, jq),
                el2 = this.getElement(_2, jq),
                x1 = el1.offset().left + el1.width() + 20 + 1, // + left padding + border width
                x2 = el2.offset().left,
                y1 = el1.offset().top + 20, // top padding + half of the height
                y2 = el2.offset().top + 20;

            var length = Math.sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            var angle = Math.atan2(y2 - y1, x2 - x1) * 180 / Math.PI;
            var transform = 'rotate(' + angle + 'deg)';

            var line = $('<div>')
                .appendTo(this.container)
                .addClass('line')
                .css({
                    'position': 'absolute',
                    'transform': transform
                })
                .offset({
                    left: x1,
                    top: y1
                })
                .attr("id", id)
                .width(length);

            if (addContextMenu)
            {
                line.attr({
                    'data-target': 'menu-' + id,
                    'context-menu': true
                });
            }

            this.drawCircle(x1, y1);
            this.drawCircle(x2, y2);
            return line;
        },

        /**
         * Converts first argument from jqLite array to jquery array and return it
         * or the first visible parent if the tree branch (containing the element) is collapsed
         *
         * @param _el the element
         * @param jq flag indicating that the element in jquery array (or in jqLite otherwise)
         * @returns {*}
         */
        getElement: function getElement(_el, jq) {
            var el = jq ? _el : $(_el[0]); // convert jqLite elements to jquery elements
            var collapsedParent = el.parents('[collapsed="true"]:last');
            return collapsedParent.length ? collapsedParent.children('div:first') : el;
        },

        drawCircle: function drawCircle(left, top) {
            var radius = 4;
            var circle = $('<div>')
                .appendTo(this.container)
                .addClass('circle')
                .css({
                    position: 'absolute',
                    "border-radius": radius + 'px',
                    width: radius * 2,
                    height: radius * 2
                })
                .offset({
                    left: left - radius,
                    top: top - radius
                });
        }
    }

});