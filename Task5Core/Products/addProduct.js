debugger;
async function AddProduct() {
  const dropDown = document.getElementById("dropDownList");
  let url = "http://localhost:39392/api/Category/getAllCategories";
  let request = await fetch(url);
  let data = await request.json();

  data.forEach((select) => {
    dropDown.innerHTML += `
      <option value="${select.categoryId}">${select.categoryName}</option>
    `;
  });
}
AddProduct();
var url1 = "http://localhost:39392/api/Product/AddProduct";
var form = document.getElementById("form");
async function fun() {
  event.preventDefault();
  var formData = new FormData(form);
  let respons = await fetch(url1, {
    method: "POST",
    body: formData,
  });
  var data = respons;
  window.location.href = "../Products/showproduct.html";
  alert("product added successfully");
}
