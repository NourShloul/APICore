const url = "http://localhost:39392/api/Category/getAllCategories";
async function GetCategories() {
  var response = await fetch(url);
  var result = await response.json();
  var container = document.getElementById("tbody");

  result.forEach((category) => {
    container.innerHTML += `   
    <tr>
      <td>${category.categoryId}</td>
      <td>${category.categoryName}</td>
      <td><img src="${category.categoryImage}" "(image not found)" style="width: 50px; height: auto;"></td>
      <td>
        <a href="../Categories/updateCategory.html"  onclick="myFunction11(${category.categoryId})">Edit</a>
      </td>
    </tr>
        `;
  });
}
function myFunction(id) {
  localStorage.setItem("categoryId", id);
  window.location.href = "../Products/showProducts.html";
}
function myFunction11(id) {
  localStorage.setItem("categoryId", id);
  window.location.href = "../Categories/updateCategory.html";
}

function myfun1() {
  window.location.href = "../Categories/addCategory.html";
}
GetCategories();
