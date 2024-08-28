async function UpdateProduct() {
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

UpdateProduct();
const n = localStorage.getItem("productId");
var url1 = `http://localhost:39392/api/Product/UpdateProduct/${n}`;
var form = document.getElementById("form");
async function fun() {
  var formData = new FormData(form);
  event.preventDefault();
  let response = await fetch(url1, {
    method: "PUT",
    body: formData,
  });
  window.location.href = "../Products/showProduct.html";
  alert("product updated successfully");
}
