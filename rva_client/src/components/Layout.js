import { Outlet, Link } from "react-router-dom";

const Layout = () => {
  return (
    <>
      <nav>
        <ul>
          <li>
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
        </ul>
      </nav>

      <Outlet />
    </>
  )
};

export default Layout;