<script>
function myFunction(id, url) {
	var x = document.getElementById("images" + id);
while (x.firstChild) {
    x.removeChild(x.firstChild);
    }
	var elem = document.createElement("img");
	elem.src = url;
	x.appendChild(elem);
}

function myFunction2(id) {
	var x = document.getElementById("images" + id);
while (x.firstChild) {
        x.removeChild(x.firstChild);
    }
}
</script>