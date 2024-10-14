import {Route, Router} from "@solidjs/router";
import ListUsersPage from "./Admin/Users/ListUsersPage";
import Login from "./Auth/Login";

const App = () => (
    <Router base="/" >
        <Route path="/admin">
            <Route path="/users" component={ListUsersPage}/>
        </Route>
        
        <Route path="/login" component={Login}/>
    </Router>
);

export default App;