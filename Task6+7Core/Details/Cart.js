var n = localStorage.getItem("cartId");
var productId = localStorage.getItem("productId");
const url = `http://localhost:2082/GetDataById/11`;
// var url = "http://localhost:2082/api/CartItems/GetToCartItem";
var tbody = document.getElementById("tbody");

async function showCart() {
  var resquest = await fetch(url);
  var response = await resquest.json();
  response.forEach((element) => {
    var row = document.createElement("tr");
    row.innerHTML += `

      <td>${element.product.productName}</td>
      <td>${element.product.price}</td>
      <td ><input id="quantity" type="number" value="${element.quantity}"/></td>
      <td>${element.product.description}</td>
       <td><button class="btn btn-outline-danger" onclick="deleteProduct(${element.cartItemId})">Remove</button><button class="btn btn-outline-primary" onclick="Update(${element.cartItemId})">Update</button></td>

      `;
    tbody.appendChild(row);
  });
}

async function deleteProduct(id) {
  var url1 = `http://localhost:2082/api/CartItems/${id}`;
  let request = await fetch(url1, {
    method: "DELETE",
  });
  alert("Item deleted successfully");
  window.location.reload();
}

async function Update(n) {
  var url2 = `http://localhost:2082/api/CartItems/${n}`;
  var input = document.getElementById("quantity").value;
  event.preventDefault();
  let response = await fetch(url2, {
    method: "PUT",
    body: JSON.stringify({
      Quantity: input,
    }),
    headers: {
      "Content-Type": "application/json",
    },
  });
  const update = await response.json();
  document.getElementById("quantity").textContent = update.quantity;
  alert("item updated successfully");
}
showCart();
