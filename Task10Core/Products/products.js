// let n = Number(localStorage.getItem("categoryId"));
var url;
// if (n) {
//   url = `http://localhost:2082/api/Products/GetProductByCatId/${n}`;
// } else {
url = "http://localhost:2082/api/Products/getAllProducts";
// }
async function getProductsByID() {
  debugger;
  var token = localStorage.getItem("jwtToken");
  if (token == null) {
    alert("you need to login, then you can see products");
  }
  var response = await fetch(url, {
    headers: {
      Authorization: `Bearer ${token}`,
    },
  });

  let response1 = await response.json();
  console.log(response1);
  var conatiner = document.getElementById("container");

  response1.forEach((product) => {
    conatiner.innerHTML += `
       <div class="card" style="width: 18rem;">
    <img src="${product.productImage}" class="card-img-top" alt="${product.productImage} this img not found" style="height: 200px; object-fit: cover;">
    <h5 class="card-title">${product.productName}</h5>
    <p class="card-text">${product.price}</p>
    <a href="../Details/details.html" class="btn btn-primary" style="background-color: #3C5B6F; border-color: #3C5B6F;" onclick="storeProductID(${product.productId})">Show Details</a>
    </div>
    </div>
    `;
  });
}
// function storeProductID(productId) {
//   localStorage.setItem("productId", productId);
//   window.location.href = "../Details/details.html";
// }
getProductsByID();
