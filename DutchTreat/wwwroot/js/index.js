console.log("page loaded");
//var form = document.getElementById("form");
//form.hidden = true;
var buyButton = $("#buyButton");
buyButton.on("click", Buy);

function Buy() {
	console.log("Buy item");
}

var pokemonStats = $(".pokemonStats li");
pokemonStats.on("click", function () {
	console.log("You clicked on: " + $(this).text())
});
