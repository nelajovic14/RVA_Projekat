import { Outlet, Link } from "react-router-dom";

const Layout = () => {
  return (
    
    <>
    <html lang="en">
    <head>
  <meta charset="utf-8"/>
  <meta name="viewport" content="width=device-width, initial-scale=1"/>
      <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/css/bootstrap.min.css"></link>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
        </head>
<body>      <nav class="navbar navbar-inverse">  <div class="container-fluid">
        <ul  class="list-inline">
          <li >
            <Link to="/Register">Register</Link>
          </li>
          <li>
            <Link to="/EditInformation">Edit</Link>
          </li>
          <li>
            <Link to="/Login">Log out</Link>
          </li>
          <li>
            <Link to="/NetoHonorari">Tabela neto honorara</Link>
          </li>
          <li>
            <Link to="/BrutoHonorari">Tabela bruto honorara</Link>
          </li>
          <li>
            <Link to="/Zaposleni">Tabela zapolsenih</Link>
          </li>
          <li>
            <Link to="/Logs">Aktivnost prijavljenog korisnika</Link>
          </li>
        </ul></div>
      </nav>

      <Outlet /></body>
</html>
    </>
  )
};

export default Layout;