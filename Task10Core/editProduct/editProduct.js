var num = Number(localStorage.getItem("productIdChoosen"));
var url = `http://localhost:2082/api/Products/${num}`;
var form = document.getElementById("form");
async function editProduct() {
  event.preventDefault();
  var formdata = new FormData(form);
  let response = await fetch(url, {
    method: "PUT",
    body: formdata,
  });
  alert("Product Updated Succefully");
}
