let url = "http://localhost:39392/api/Category/AddCategory";
var form = document.getElementById("form");
async function fun() {
  event.preventDefault();
  var formData = new FormData(form);
  let response = await fetch(url, {
    method: "POST",
    body: formData,
  });
  window.location.href = "../Admin/Admin.html";
  alert("category added successfully");
}
