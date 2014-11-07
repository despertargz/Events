function formatDate(date) {
    if (date) {
        return moment(date).format("MM/DD/YY");
    }
    else {
        return null;
    }
}

function formatPriority(priority) {
    var priorityLookup = {
        1: "Very Low",
        2: "Low",
        3: "Normal",
        4: "High",
        5: "Very High"
    };

    return priorityLookup[priority];
}

function formatStatus(status) {
    var statusLookup = {
        0: "Pending",
        1: "Working",
        2: "Completed",
        3: "Waiting",
        4: "Blocked",
        5: "Cancelled"
    };

    return statusLookup[status];
}

// create event

function getDynamicData(rows, type) {
    var data = null;

    if (type == "Object") {
        data = {};
    }
    else {
        data = [];
    }

    var myType = type;
    rows.each(function (index, row) {
        if (!$(row).hasClass("child-container") && $(row).find(".dataform-type").val() == "Text") {
            var key = $(row).find(".dataform-key").val();
            var value = $(row).find(".dataform-value").val();
            if (myType == "Object") {
                data[key] = value;
            }
            else {
                data.push(value);
            }
        }
        else if (!$(row).hasClass("child-container")) {
            var key = $(row).find(".dataform-key").val();
            var type = $(row).find(".dataform-type").val();
            var value = getDynamicData($(row).next().children().children(":not(.child-container)"), type);
            if (myType == "Object") {
                data[key] = value;
            }
            else {
                data.push(value);
            }
        }
        else {
            console.log("unknown type");
        }
    });

    return data;
}

function createAnEvent() {
    var data = new Object();
    data.Priority = $("#priority").val();
    data.Subject = $("#subject").val();
    data.Description = $("#description").val();
    data.Due = $("#duedate").val();
    data.Labels = $("#labels .label").map(function (i, v) { return $(v).html(); }).toArray();
    data.Data = getDynamicData($("#dataform-root").children(), "Object");

    $.ajax({
        url: '/events',
        method: 'POST',
        data: JSON.stringify(data),
        contentType: "application/json",
        success: function () {
            $("#example").DataTable().ajax.reload()
            $("#modalCreateEvent").modal('toggle');
            $("#modalCreateEvent").find('form')[0].reset()
        }
    });
}

function reload() {
    $("#example").DataTable().ajax.reload();
}

function addTag() {
    var newLabel = $("#newLabel").val();
    $("#newLabel").val('');
    $("#newLabel").focus();
    $("#labels").append('<p style="margin-left: 3px; display:inline; font-size:20px;"><span style="background-color:darkblue;" class="label label-default">' + newLabel + '</span></p>');
        
}

function setStatus() {
    var eventId = $("#details").data('eventId');
    var newStatus = $("#status").val();

    $.ajax({
        url: '/events/' + eventId + "/status?status=" + newStatus,
        method: 'POST',
        success: function () {
            $("#example").DataTable().ajax.reload()
        }
    });
}

function setPriority() {
    var eventId = $("#details").data('eventId');
    var newPriority = $("#priority").val();

    $.ajax({
        url: '/events/' + eventId + "/priority?priority=" + newPriority,
        method: 'POST',
        success: function () {
            $("#example").DataTable().ajax.reload()
        }
    });
}

function toggleModal_createEvent() {
    $("#modalCreateEvent").modal();
    appendRow(false, $("#dataform-root"));
}

// main
$(document).ready(function () {
    var t = $('#example').dataTable({
        "ajax": "/events",
        "columns": [
            { "data": "Subject", width: "75%" },
            { "data": "Status", width: "5%", render: formatStatus },
            { "data": "Priority", width: "5%", render: formatPriority },
            { "data": "Due", width: "5%", render: formatDate },
            { "data": "Created", width: "5%", render: formatDate },
            { "data": "Updated", width: "5%", render: formatDate },

        ]
    });

    $('#example tbody').on('click', 'tr', function () {
        var item = t.fnGetData(this);
        var details = $("#details");

        details.data('eventId', item._id);
        $("#description").html(item.Description);

        window.viewModel.detail(item);



    });
});

function ViewModel() {
    var self = this;
    self.statuses = [
        { id: 0, name: "Pending" },
        { id: 1, name: "Working" },
        { id: 2, name: "Completed" },
        { id: 3, name: "Waiting" },
        { id: 4, name: "Blocked" },
        { id: 5, name: "Cancelled" }
    ];

    self.priorities = [
        { id: 1, name: "Very Low" },
        { id: 2, name: "Low" },
        { id: 3, name: "Normal" },
        { id: 4, name: "High" },
        { id: 5, name: "Very High" }
    ];

    self.detail = ko.observable(ko.mapping.fromJS({
        _id: '',
        Status: '',
        Priority: '',
        Description: '',
        Title: '',
        Tags: '',
        Data: '',
        Created: '',
        Updated: '',
        Completed: ''
    }));

    self.detail().Status.subscribe(function (newStatus) {
        console.log("status changed to: " + newStatus);
    });

    self.setStatus = function() {
        $.post("/events/" + self.detail()._id + "/status?status=" + self.detail().Status);
        reload();
    };

    self.setPriority = function () {
        $.post("/events/" + self.detail()._id + "/priority?priority=" + self.detail().Priority);
        reload();
    };
}

window.viewModel = new ViewModel();
ko.applyBindings(window.viewModel);

