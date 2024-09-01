var url = "http://localhost:2082/api/Categories/getAllCategories";

async function getAllCategories() {
  var request = await fetch(url);
  var response = await request.json();
  var cards = document.getElementById("container");
  response.forEach((category) => {
    cards.innerHTML += `
    <div class="card" style="width: 18rem;">
    <img src="${category.categoryImage}" class="card-img-top" alt="${category.categoryImage} this img not found" style="height: 200px; object-fit: cover;">
    <div class="card-body">
    <h5 class="card-title">${category.categoryName}</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <a href="../Products/products.html" class="btn btn-primary" onclick="storeCategoryID(${category.categoryId})">Show Products</a>
    </div>
    </div>
    `;
  });
}
getAllCategories();
function storeCategoryID(x) {
  localStorage.categoryId = x;
}
