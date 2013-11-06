/// <reference path="../../knockout-2.3.0.js" />
/// <reference path="../../jquery-2.0.3.js" />

$(function () {
    ko.applyBindings(viewModel);
    viewModel.loadCategories();
});

function category(category) {
    this.CategoryId = ko.observable(category.CategoryId);
    this.Name = ko.observable(category.Name);
    this.Description = ko.observable(category.Description);
    this.CreatedDate = ko.observable(category.CreatedDate);
}

var viewModel = {
    categories: ko.observableArray([]),

    loadCategories: function () {
        var self = this;
        $.getJSON(
            '/api/category',
            function (categories) {
                self.categories.removeAll();
                $.each(categories, function (index, item) {
                    self.categories.push(new category(item));
                });
            }
        );
    },
};

