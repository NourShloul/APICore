var url = "http://localhost:39392/api/Product/AllProducts";
async function GetProducts() {
  var response = await fetch(url);
  var result = await response.json();
  console.log(result);
  var container = document.getElementById("tbody");

  result.forEach((category) => {
    container.innerHTML += `    
    <tr>
      <td>${category.productId}</td>
      <td>${category.productName}</td>
      <td>${category.price}</td>
      <td><img src="${category.productImage}" " (image not found)" style="width: 50px; height: auto;"></td>
      <td>
        <a href="../Products/updateProduct.html"  onclick="clic12k(${category.productId})">Edit</a>
      </td>
    </tr>
    `;
  });
}
function clic11k(id) {
  localStorage.setItem("productId", id);
  window.location.href = "../Products/showProducts.html";
}

function clic12k(id) {
  localStorage.setItem("productId", id);
  window.location.href = "../Products/updateProduct.html";
}

function myfun1() {
  window.location.href = "../Products/addProduct.html";
}
GetProducts();
