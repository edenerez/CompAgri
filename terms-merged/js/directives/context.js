angular.module('TermsDataEntry')
.directive('context', ['$document', '$compile', function ($document, $compile) {
    return function (scope, element, attr) {

        var menu = $compile("<context-menu-display></context-menu-display>")(scope); // creating the context-menu element to show (See directive above)
        angular.element(document.getElementsByTagName('body')[0]).append(menu); // Append it to DOM

        // close the context menu when another is open
        $document.on('contextmenu', function (event) {
            menu.removeClass("open").addClass("closed");
        });

        // Display the menu when invoked by right clicking
        element.on('contextmenu', function (event) {
            event.preventDefault(); // avoid the browser context menu to be open
            event.stopPropagation(); // so it doesn't trigger the closing of the menu latter

            // Set the menu where it was asked (rigth click position)
            menu.css({
                top: event.pageY + "px",
                left: event.pageX + "px"
            });

            // Finally open the menu
            menu.removeClass("closed").addClass("open");

            // timeout because when mouse button is released, the click event is trigered closing the menu.
            // So we give time to the user to release the button before registering the event
            setTimeout(function () {
                $document.one('click', function (event) {
                    menu.removeClass("open").addClass("closed");
                });
            }, 500);
        });

    };
}])

.directive('contextMenuDisplay', function () {
    return {
        replace: true,
        // No drag is necesary to avoid weird behavior
        template: "<div class='closed panel context-menu '  tab-index='1'  data-nodrag >" +
            "<ul>" +
            "<li ng-repeat='option in contextmenu' >" +
            // data-option instead of ng-click because something was avoiding the ng-click
            // event to be triggered. Registered the event manually
            "<a  data-option='{{$index}}'>{{ option.text }}</a>" +
            "</li></ul></div>",
        link: function (scope, element) {
            // Whenever a click is made inside the menu, it  checks where it was made
            element.on('click', function (e) {
                // If it was made inside an "<a>" element, so a option was selected
                if (e.target.tagName.toLowerCase() === "a") {

                    var $this = angular.element(e.target);
                    // We pick the option index in the context menu and trigger the proper action
                    scope.contextmenu[$this.attr("data-option")].action(scope);
                }
            });
        }
    };
})