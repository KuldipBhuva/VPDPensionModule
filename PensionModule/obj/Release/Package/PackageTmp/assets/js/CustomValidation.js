
function showErr(objId, err) {
    if (document.getElementById("err_" + objId) != null) {
        removeErr("err_" + objId);
    }
    $('#' + objId).parent().append('<span id="err_' + objId + '" class="errMsg">' + err + '</span>')
}


function removeErr(objId) {
    if (document.getElementById(objId) != null) {
        $('#' + objId).remove();
    }

}