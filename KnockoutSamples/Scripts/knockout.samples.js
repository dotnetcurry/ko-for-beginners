/// <reference path="knockout-2.3.0.debug.js" />
viewModel = {
    lookupCollection : ko.observableArray()
};

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/Lookups/GetIndex",
    }).done(function (data) {
        $(data).each(function (index, element) {
            var mappedItem = 
                {
                    Id: ko.observable(element.Id),
                    Key: ko.observable(element.Key),
                    Value: ko.observable(element.Value),
                    Mode: ko.observable("display")
                };
            viewModel.lookupCollection.push(mappedItem);
        });
        ko.applyBindings(viewModel);
    }).error(function (ex) {
        alert("Error");
    });

    $(document).on("click", ".kout-edit", null, function (ev) {
        var current = ko.dataFor(this);
        current.Mode("edit");
    });

    $(document).on("click", ".kout-update", null, function (ev) {
        var current = ko.dataFor(this);
        saveData(current);
        current.Mode("display");
    });
    
    $(document).on("click", "#create", null, function (ev) {
        var current = {
            Id: ko.observable(0),
            Key: ko.observable(),
            Value: ko.observable(),
            Mode: ko.observable("edit")
        }
        viewModel.lookupCollection.push(current);
    });

    function saveData(currentData) {
        var postUrl = "";
        var submitData = {
            Id : currentData.Id(),
            Key : currentData.Key(),
            Value: currentData.Value()

        };

        if (currentData.Id && currentData.Id() > 0) {
            postUrl = "/Lookups/Edit"
        }
        else {
            postUrl = "/Lookups/Create"
        }
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: postUrl,
            data: JSON.stringify(submitData)
        }).done(function (id) {
            currentData.Id(id);
        }).error(function (ex) {
            alert("ERROR Saving");
        })
    }

});