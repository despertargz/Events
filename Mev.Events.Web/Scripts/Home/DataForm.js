function add_OnClick() {
    var parent = $(this).closest(".form-group");
    appendRow(false, parent);
}

function appendRow(increaseLevel, parent) {
    var level = parent.data("level");
    var currentType = null;
    if (increaseLevel) {
        level += 1;

        // when increasing level we want to look at the type of the CURRENT ROW
        currentType = parent.find(".dataform-type").val();
    }
    else {
        // when appending to an already indented row we want to look at the PARENT ROW type to see if it is list of object
        currentType = parent.closest(".child-container").prev().find(".dataform-type").val();
    }


    var newRow = null;
    if (currentType == "List") {
        newRow = $($("#templateValueFormRow").text());
    }
    else {
        newRow = $($("#templatePropertyFormRow").text());
    }

    newRow.data("level", level);
    newRow.find("select").change(updateType);
    newRow.find("button").click(add_OnClick);

    if (increaseLevel) {
        parent.next(".form-group").find(".form-horizontal").append(newRow);
    }
    else {
        parent.closest(".form-horizontal").append(newRow);
    }

    if (currentType == "List") {
        newRow.find(".dataform-value").focus();
    }
    else {
        newRow.find(".dataform-key").focus();
    }
    event.preventDefault();
}

function updateType() {
    var valueType = $(this).val();
    var valueWrapper = $(this).parent().siblings('.dataform-value-wrapper');
    var value = valueWrapper.children();
    var formGroup = $(this).closest(".form-group");

    value.prop('disabled', true);
    $(this).prop('disabled', true);
    var nextLevel = formGroup.data("level") + 1;

    appendRow(true, formGroup);
}

function createForm() {
    var mainForm = $('<form id="mainForm" role="form" class="form-horizontal" data-level="0"></form>');
    appendRow(false, mainForm);
    return mainForm;
}