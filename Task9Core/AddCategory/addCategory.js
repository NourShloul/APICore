var url = "http://localhost:2082/api/Categories";
var form = document.getElementById("form");

async function addCategory() {
  debugger;
  var formSwager = new FormData(form);
  event.preventDefault();
  let response = await fetch(url, {
    method: "POST",
    body: formSwager,
  });

  let data = response;
}
