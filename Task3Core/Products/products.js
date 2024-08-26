let n = Number(localStorage.getItem("categoryIdChoosen"));

async function getProductsByID() {
  var url;
  if (n == null || n == 0) {
    url = "http://localhost:2082/api/Products/getAllProducts";
  } else {
    url = `http://localhost:2082/api/Products/GetProductByCatId/${n}`;
  }
  let request = await fetch(url);
  let response = await request.json();
  console.log(response);
  var conatiner = document.getElementById("container");

  response.forEach((product) => {
    conatiner.innerHTML += `
       <div class="card" style="width: 18rem;">
    <img src="${product.productImage}" class="card-img-top" alt="${product.productImage} this img not found" style="height: 200px; object-fit: cover;">
    < class="card-body">
    <h5 class="card-title">${product.productName}</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <a href="../Details/details.html" class="btn btn-primary" style="background-color: #3C5B6F; border-color: #3C5B6F;" onclick="storeProductID(${product.productId})">Discover the product details!</a>
    </div>
    </div>
    `;
  });
}
getProductsByID();

function storeProductID(productId) {
  localStorage.productIdChoosen = productId;
}
