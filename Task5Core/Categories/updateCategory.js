const n = localStorage.getItem("categoryId");
var url = `http://localhost:39392/api/Category/UpdateCategory/${n}`;
var form = document.getElementById("form");
async function fun2() {
  var formData = new FormData(form);
  event.preventDefault();
  let response = await fetch(url, {
    method: "PUT",
    body: formData,
  });
  window.location.href = "../Admin/Admin.html";
  alert("Category Uploded Successfully");
}
