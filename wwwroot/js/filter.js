// =============== VARIABLES ===============

var filteredList = document.getElementById("filteredList");
var selectedInput = null;
var activeSearch = document.getElementsByClassName("activeSearch");
$createBtn = $("#createBtn");
var validationDict = {
    "Author": false,
    "Language": false,
    "Publisher": false,
    "Category": false,
    "Genre": false,
    "Cover": false
}


// =============== EVENTS ===============

$createBtn.on("click", function (e) {
    var valid = true;
    for (var key in validationDict) {
        valid = valid && validationDict[key];
        if (!validationDict[key]) {
            $(`#${key}`).next().text(`Select valid ${key} or create new one.`);
        }
    }
    if (!valid) {
        e.preventDefault();
    }
})

for (var i = 0; i < activeSearch.length; i++) {
    activeSearch[i].addEventListener("blur", async function () {  
        validateInput(this);
        await sleep(125);
        clearNode(filteredList);
    }, false);
}

filteredList.addEventListener("click", function (e) { inputSet(e) }, false);

$(".activeSearch").on("input", function (e) {

    e.preventDefault();
    filteredList.innerHTML = "";
    var value = this.value;
    if (value) {
        selectedInput = this;
        var $this = $(this);
        var $list = $this.next();
        var matchedOptions = [];
        moveFilterWindow(filteredList, this);

        // Use each() to cast jQuery object into javascript
        $($list).each(function () {
            matchedOptions = searchAlgorithm(this, value, 5);
        })
        fillNode(filteredList, matchedOptions);
    }
    else {
        filteredList.style.display = "none";
    }

})


$(".activeSearch").on("dblclick", function (e) {
    filteredList.innerHTML = "";
    selectedInput = this;
    moveFilterWindow(filteredList, this);
    var $list = $(this).next();
    var matchedOptions = [];
    $list.each(function () {
        matchedOptions = searchAlgorithm(this, null, 10)
    })

    fillNode(filteredList, matchedOptions);
})

$("#inputCategory").on("change", function () {
    updateGenresList();
    
})

// =============== FUNCTIONS ===============

function validateInput(inputNode) {
    var inputId = inputNode.id;
    var key = inputId.replace("input", "");
    var valid = false;
    var tags = filteredList.getElementsByTagName("p");
    for (var i = 0; i < tags.length; i++) {
        var valid = tags[i].textContent.toLowerCase() == inputNode.value.toLowerCase();
        if (valid) {
            break;
        }
    }
    validationDict[key] = valid;
}

function searchAlgorithm(listNode, inputValue = null, listSize = null) {
    var sortedResults = [];
    if (inputValue) {
        var results = {};
        for (var i = 0; i < listNode.options.length; i++) {
            var optionText = listNode.options[i].text;
            var splittedValue = inputValue.trim().split(" ");
            splittedValue.forEach(function (word) {
                if (optionText.toLowerCase().includes(word.toLowerCase())) {
                    var resultValue = results[optionText];
                    if (resultValue) {
                        results[optionText] = resultValue + word.length;
                    }
                    else {
                        results[optionText] = word.length;
                    }
                }

            })
        }
        sortedResults = sortDict(results);
    }
    else {
        var results = [];
        for (var i = 0; i < listNode.options.length; i++) {
            results[i] = listNode.options[i].text;
        }
        sortedResults = results.sort();
    }

    if (listSize) {
        sortedResults = sortedResults.slice(0, listSize);
    }
    return sortedResults;

}

function sortDict(dict) {
    var sorted = [];
    var items = Object.keys(dict).map(function (key) {
        return [key, dict[key]];
    });

    // Sort the array based on the second element
    items.sort(function (first, second) {
        return second[1] - first[1];
    });

    for (var i = 0; i < items.length; i++) {
        sorted[i] = items[i][0];
    }

    return sorted;
}

function inputSet(e) {
    if (e.target.tagName == "P") {
        e.stopPropagation();
        selectedInput.value = e.target.textContent;
        validateInput(selectedInput);
        clearNode(filteredList);
        $validationNode = $(selectedInput).next().next().next().text("");
        if (selectedInput.id == "inputCategory") {
            updateGenresList();
        }
    }
}


function moveFilterWindow(filterNode, node) {
    var $this = $(node);
    var inputPos = $this.position();
    var inputHeight = $this.height();
    var inputWidth = $this.outerWidth();
    filterNode.style.left = `${inputPos["left"]}px`;
    filterNode.style.top = `${inputPos["top"] + inputHeight + 5}px`;
    filterNode.style.width = `${inputWidth}px`
}

function fillNode(filterNode, matchedOptions) {
    if (matchedOptions.length > 0) {
        filterNode.style.display = "block";
        matchedOptions.forEach(function (option) {
            var newEl = document.createElement("p");
            var text = document.createTextNode(option);
            newEl.appendChild(text);
            newEl.className = "filtered my-0"
            filterNode.appendChild(newEl);
        })
    }
    else {
        clearNode(filterNode);
    }
}

function clearNode(node) {
    node.style.display = "none";
    node.innerHTML = "";
}

function sleep(ms) {
    return new Promise(
        resolve => setTimeout(resolve, ms)
    );
}

function updateGenresList() {

    // Currently selected category value get
    // ------------------
    var $inputGenre = $("#inputGenre");
    $inputGenre.val('');
    var categorySelected = document.getElementById("inputCategory").value;
    var list = document.getElementById("listCategory");
    var id = null;
    for (var i = 0; i < list.options.length; i++) {
        if (list.options[i].text == categorySelected) {
            id = list.options[i].value;
            break;
        }
    }
    // Initialize variables
    // ------------------
    var $genresList = $("#listGenres");
    $genresList.html('');

    // start asynchronous call
    // ------------------
    if (id) {
        $inputGenre.removeAttr("disabled");

        $.ajax({
            url: "/Admin/Book/GenreGet/" + id,
            type: "GET",
            dataType: "text",
            success: function (data) {

                // Parse json data
                // ------------------
                var results = JSON.parse(data);

                // If no data is found don't add section with genres
                // ------------------
                if (results.length > 0) {
                    for (var id in results) {
                        $genresList.append(`<option value=${id}>${results[id].text}</option>`);
                    }

                }
            }
        })
    }
    else {
        $inputGenre.attr("disabled", "disabled");
    }


    
}