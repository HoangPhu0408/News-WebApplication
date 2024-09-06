


var display_news = document.getElementsByClassName("card-text");
for (let item in display_news) {
    if (display_news[item].innerHTML.length > 36)
    {
        var temp_name = display_news[item].innerHTML.substr(0,140);
        display_news[item].innerHTML = temp_name + "...";  
    }
}

