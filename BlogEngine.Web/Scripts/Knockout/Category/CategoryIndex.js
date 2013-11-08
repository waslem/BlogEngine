/// <reference path="../../knockout-2.3.0.js" />
/// <reference path="../../jquery-2.0.3.js" />
/// <reference path="../../knockout.mapping-latest.js" />

//$(function () {
//    ko.applyBindings(viewModel);
//    viewModel.loadCategories();
//});

var viewModel = function () {
    var self = this;
    self.CategoryId = ko.observable("0");
    self.Name = ko.observable("");
    self.Description = ko.observable("");
    self.CreatedDate = ko.observable("");

    var CategoryData = {
        CategoryId: self.CategoryId,
        Name: self.Name,
        Description: self.Description,
        CreatedDate: self.CreatedDate
    };

    self.Categories = ko.observableArray([]);

    GetCategories();

    self.save = function () {
        $.ajax({
            type: 'POST',
            url: '/api/category',
            data: ko.toJSON(CategoryData),
            contentType: 'application/json',
            success: function (data) {
                self.CategoryId(data.CategoryId);
                $('#category-modal').modal('hide');
                GetCategories();
            },
            error: function () {
                alert("Error");
            }
        });
    }

    self.remove = function (category) {
        $.ajax({
            type: 'DELETE',
            url: '/api/category/' + category.CategoryId,
            success: function (data) {
                GetCategories();
            }
        });
    }

    function GetCategories() {
        $.ajax({
            type: 'GET',
            url: '/api/category',
            contentType: 'application/json; charset=utf-8',
            dataType:'json',
            success: function(data) {
                self.Categories(data);
            }
        });
    }

}

ko.applyBindings(new viewModel());

//var viewModel = {
//    categories: ko.observableArray([]),
//    category: ko.observable(),

//    loadCategories: function () {
//        var self = this;
//        $.getJSON(
//            '/api/category',
//            function (categories) {
//                self.categories.removeAll();
//                $.each(categories, function (index, item) {
//                    self.categories.push(new category(item));
//                });
//            }
//        );
//    },
//    save: function () {
//        this.Response = null;
//        $.ajax({
//            url: '/api/category',
//            contentType: 'application/json',
//            type: 'POST',
//            data: ko.toJSON(category),
//            dataType: 'json'
//        });
//    }
//};

