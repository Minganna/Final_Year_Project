function password()
{
var myInput = document.getElementById("pwd");
if (myInput.value=="the blaze") {
var node = document.createElement("LI");
node.addEventListener('click', function(e) {
    window.location='https://www.youtube.com/watch?v=lzehZDboFW0'
    });
  var textnode = document.createTextNode("Click here for the Monster of Florence documentary");
  node.appendChild(textnode);
  document.getElementById("secrets").appendChild(node);
}
}
