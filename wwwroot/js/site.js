// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


// ============ ADMIN/GENRE ============

// Create function to call when page is loaded and Category is changed
// ------------------

function updateGenresList() {

    // Currently selected category value get
    // ------------------
    var categorySelected = document.getElementById("ddlCategories").value;

    // Initialize variables
    // ------------------
    var $genresList = $("#genresList");
    $genresList.html('');
    var genresContent = "";

    // start asynchronous call
    // ------------------
    $.ajax({
        url: "/Admin/Genre/GenreGet/" + categorySelected,
        type: "GET",
        dataType: "text",
        success: function (data) {

            // Parse json data
            // ------------------
            results = JSON.parse(data);

            // If no data is found don't add section with genres
            // ------------------
            if (results.length > 0) {

                // Create content of genres list
                // ------------------
                genresContent += "<hr />"
                genresContent += "<h4>Existing Genres</h4>"
                genresContent += "<div class='offset-2 col-5 py-2 px-0'>"
                genresContent += "<div>"
                for (var i in results) {
                    genresContent += "<div class='border-bottom py-1' >" + results[i].text + "</div>";
                }
                genresContent += "</div>"
                genresContent += "</div>"
                genresContent += "</div>"

                // Add content to website
                // ------------------
                $genresList.html(genresContent);
            }
        },
        error: function () {
            // In case of json file loading error display error message
            // ------------------
            $genresList.html("Genres loading error");
        }

    })
}

/*  updateGenresList has to be called 2 times:
    1. When page is loaded
    2. When category is selected from drop-down list
*/

// 1#
// ------------------
$(function () {
    updateGenresList();
});

// 2#
// ------------------
$("#ddlCategories").on("change", function () {
    updateGenresList();
});