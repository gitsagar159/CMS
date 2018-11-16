$(document).ready(function () {
    $("#subSreamDiv").hide();
});

$(window).load(function () {
    $("#subSreamDiv").hide();
});

function submitForm() {

    if (CourseFormValidate()) {
        var Id = $("#id").val() == undefined || $("#id").val() == "" ? 0 : $("#id").val();
        var name = $("#Name").val();
        var email = $("#Email").val();
        var contact = $("#Contact").val();
        var gender = $('input[name=Gender]:checked').val();
        var qualificationId = $("#QualificationId").val();
        var mainStreamId = $("#MainStreamId").val();
        var subStreamId = $("#subStream_dorpdown").val();


        var fd = new FormData();
        fd.append('id', Id);
        fd.append('Name', name);
        fd.append('Email', email);
        fd.append('Contact', contact);
        fd.append('Gender', gender);
        fd.append('QualificationId', qualificationId);
        fd.append('MainStreamId', mainStreamId);
        fd.append('SubStreamId', subStreamId);


        $.ajax({
            url: '/Home/AddUserDetails',
            data: fd,
            processData: false,
            contentType: false,
            type: 'POST',
            success: function (data) {
                //alert(data);
                if (data == "Add") {
                    toastr.success('New Course added succsessfuly');
                    window.setTimeout(function () { location.reload() }, 3000)
                }
                if (data == "Update") {
                    toastr.success('Course Updated succsessfuly');
                    window.setTimeout(function () { window.location.href = "/Home/CourseDetails" }, 3000)
                }
            }
        });

    }
}


function CourseFormValidate() {
    var IsValid = true;
    var courseName = $("#CourseName").val();
    var mainStreamDorpdown = $("#mainStream_dorpdown").val();
    var subStreamDorpdown = $("#subStream_dorpdown").val();
    var eligibility = $("#eligibility_dorpdown").val();
    if (courseName == "" || courseName == undefined) {
        alert("Please Insert Course Name");
        return false;
    }
    if (mainStreamDorpdown == "" || mainStreamDorpdown == undefined) {
        alert("Please select main Stream");
        return false;
    }
    if (eligibility == "" || eligibility == undefined) {
        alert("please select eligibilty");
        return false;
    }

    return IsValid;
}



$("#MainStreamId").change(function () {
    debugger;
    alert(1);
    var mid = $("#MainStreamId").val();

    $.ajax({
        type: 'POST',
        url: '/Home/GetSubStreamData',
        data: { Mid: mid },
        success: function (data) {
            jsonObj = jQuery.parseJSON(data);
            if (jsonObj.length > 0) {
                $("#subSreamDiv").show();
                var i;
                for (i = 0; i < jsonObj.length; i++) {
                    $("#subStream_dorpdown").append(new Option(jsonObj[i].SubStreamName, jsonObj[i].Sid));
                }
            }
            else {
                $("#subSreamDiv").hide();
            }



        }
    });

});
