var quantity = document.getElementById("quantity");
function CartId() {
  localStorage.setItem("userId", 1);
  localStorage.setItem("cartId", 1);
}
async function Add() {
  const url = "http://localhost:2082/api/CartItems";
  var data = {
    cartId: localStorage.getItem("cartId"),
    productId: localStorage.getItem("productId"),
    quantity: quantity.value,
  };
  var resquest = await fetch(url, {
    method: "POST",
    body: JSON.stringify(data),
    headers: {
      "Content-Type": "application/json",
    },
  });
}
