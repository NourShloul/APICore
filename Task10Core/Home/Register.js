var url = "http://localhost:2082/api/Users/Register";
var form = document.getElementById("form");
async function Register() {
  event.preventDefault();
  let formData = new FormData(form);
  let response = await fetch(url, {
    method: "POST",
    body: formData,
  });
  alert("you registered successfuly");
  window.location.href = "../Home/Login.html";
}
