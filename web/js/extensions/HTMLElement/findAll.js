define(["js/extensions/test-proto"], test => {

    test(HTMLElement, ["findAll"]);

    HTMLElement.prototype.findAll = function(search) {
        return this.querySelectorAll(search);
    }
});