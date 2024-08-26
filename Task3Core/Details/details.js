let n = Number(localStorage.getItem("productIdChoosen"));
const url = `http://localhost:2082/api/Products/GetProductById/${n}`;

async function getProductsDetails() {
  let request = await fetch(url);
  let response = await request.json();

  var conatiner = document.getElementById("container");

  conatiner.innerHTML = `
      <div class="card" style="width: 18rem;">
  <img src="${response.productImage}" class="card-img-top" alt="${response.productImage} this image is not found">
  <div class="card-body">
    <h5 class="card-title">${response.productName}</h5>
    <p class="card-text">${response.price}$</p>
  </div>
</div>
      `;
}

getProductsDetails();
