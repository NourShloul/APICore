function CartId() {
  localStorage.setItem("userId", 1);
  localStorage.setItem("cartId", 1);
}

const n = localStorage.getItem("productId");
var url = `http://localhost:2082/api/Products/GetProductById/${n}`;

async function GetProducts() {
  var response = await fetch(url);
  var result = await response.json();
  var container = document.getElementById("container");
  container.innerHTML = `    <div class="card" style="width: 18rem;">
  <img class="card-img-top" src="${result.productImage}" alt="${result.productImage} (image not found)">
  <div class="card-body">
      <h5 class="card-title">${result.productName}</h5>
      <p class="card-text">${result.price}</p>
      <input type="number" id="quantity" />    
      <button class="btn btn-success" onclick="Add()">add to Cart </button>
  </div>
  </div>
  ;`;
}

async function Add() {
  var quantity = document.getElementById("quantity");
  const url2 = "http://localhost:2082/api/CartItems";
  var data = {
    cartId: 11,
    productId: localStorage.getItem("productId"),
    quantity: quantity.value,
  };
  var resquest = await fetch(url2, {
    method: "POST",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
  alert("the item added to the cart successfully");
  window.location.href = "../Details/Cart.html";
}

function clic11k(id) {
  localStorage.setItem("productId", id);

  window.location.href = "../Details/Cart.html";
}

GetProducts();
CartId();
