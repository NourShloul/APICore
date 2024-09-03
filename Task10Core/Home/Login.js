// login with from body way

// var url = ""; //login url
// async function Login() {
//   event.preventDefault();
//   // tha data will give it from body in swagger
//   var data = {
//     username: document.getElementById("username").value,
//     email: document.getElementById("email").value,
//     password: document.getElementById("password").value,
//   };
//   var response = await fetch(url, {
//     method: "POST",
//     body: JSON.stringify(data),
//     headers: {
//       "Content-Type": "application/json",
//     },
//   });
// }

var url = "http://localhost:2082/api/Users/Login";
var form = document.getElementById("form");
async function Login() {
  event.preventDefault();
  let formData = new FormData(form);
  let response = await fetch(url, {
    method: "POST",
    body: formData,
  });
  alert("you login successfuly");
  window.location.href = "../Home/index.html";
  var result = await response.json();
  localStorage.setItem("jwtToken", result.token);
}
