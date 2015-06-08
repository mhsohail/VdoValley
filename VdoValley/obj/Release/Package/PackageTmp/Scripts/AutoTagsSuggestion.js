selectReference = null;
$(document).on("change", "#SelectedCategory", function () {
    //alert($(this).parents("td").siblings(".video-title").text().trim());
    selectReference = $(this);
    getTags(selectReference.parents("td").siblings(".video-title").text().trim(), "ImportDailymotionVideos");
    //alert(selectReference.parents("td").siblings(".video-title").text().trim());
});

function getTags(title, pageName) {
    
    var tokens = title.split(" ");
    var tags = [];

    $(tokens).each(function (key, value) {

        for (var j = key; j < tokens.length; j++) {
            var token = "";
            for (var k = key; k <= j; k++) {
                token = token + " " + tokens[k];
            }

            if (token != " ") {
                var tag = {
                    Name: token.trim()
                };
                tags.push(tag);
            }
        }
    });

    $.ajax({
        type: "POST",
        url: "/Tags/GetTags",
        data: JSON.stringify(tags),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            handleData(response, pageName);
            //alert(JSON.stringify(response));
        },
        error: function (xhr, status, error) {
            handleData(xhr.responseText, pageName);
            //alert(JSON.stringify(response));
        }
    });
}

function handleData(data, pageName) {
    //console.log(data);
    $(data).each(function (key, val) {
        appendTag(val.Name, pageName);
    });
}

function appendTag(tagName, pageName) {
    
    if (pageName == "createVideo" || pageName == "editVideo") {
        if ($("#suggested-tags").find("#" + tagName.replace(/[\s']/g, "_")).length > 0) return;
        var li = "<li class=\"active\" id=\"" + tagName.replace(/[\s']/g, "_") + "\"><a href=\"#\" style=\"background-color: #EEEEEE;padding:5px;color:grey;text-transform: lowercase;\">" + tagName + "</a></li>";
        $("#suggested-tags").append(li);
        
        if ($("input[name='Tags']").val().length == 0) {
            $("input[name='Tags']").val(tagName.replace(/'/g, "%27"));
        } else {
            $("input[name='Tags']").val($("input[name='Tags']").val() + "," + tagName.replace(/'/g, "%27"));
        }

    } else if (pageName == "ImportDailymotionVideos") {
        if (selectReference != null) {
            var suggestedTags = selectReference.parents("td").siblings(".suggested-tags");
            
            if (suggestedTags.find("." + tagName.replace(/[\s']/g, "_")).length > 0) return;
            var li = "<li class=\"active\" id=\"" + tagName.replace(/[\s']/g, "_") + "\"><a href=\"#\" style=\"background-color: #EEEEEE;padding:5px;color:grey;text-transform: lowercase;\">" + tagName + "</a></li>";
            suggestedTags.append(li);
            
            if (selectReference.siblings("input[name='Tags']").val().length == 0) {
                selectReference.siblings("input[name='Tags']").val(tagName.replace(/'/g, "%27"));
            } else {
                selectReference.siblings("input[name='Tags']").val(selectReference.siblings("input[name='Tags']").val() + "," + tagName.replace(/'/g, "%27"));
            }
        }
    }
}

$(document).on("keyup", "input[name='InputTags']", function (e) {
    var tag = $(this).val();
    if (tag.substr(-1) == ",") {
        appendTag(tag.substring(0, tag.length - 1), "ImportDailymotionVideos");
        $(this).val("");
    }
});

$(document).on("click", ".suggested-tags .active", function (e) {
    e.preventDefault();

    // get the tagName which is clicked and needs to be removed
    var tagName = $(this).find("a").text();

    // get hidden Tags input value
    var tags = selectReference.siblings("input[name='Tags']").val();

    // split the value by comman to make array
    tagArray = tags.split(",");

    // get index of tagName to be removed
    var index = tagArray.indexOf(tagName);

    // remove the tag from array
    tagArray.splice(index, 1);

    // join the updated array to make updated comma separated string
    var updatedTags = tagArray.join();

    // update the hidden Tags field with new comman separated tags string
    selectReference.siblings("input[name='Tags']").val(updatedTags);

    // remove the clicked tag
    $(this).remove();
});