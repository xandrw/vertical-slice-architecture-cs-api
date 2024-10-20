import Brand from "./Brand";
import SidebarLink from "./SidebarLink";
import {auth} from "../../Store/auth";

const Sidebar = () => {
    return (
        <aside
            class="hidden w-64 overflow-y-auto bg-white md:block flex-shrink-0 border-r border-gray-200 text-gray-500">
            <Brand/>
            <nav class="mb-6">
                <SidebarLink route="/admin/users" display={auth.isAdmin} name="Users"/>
                <SidebarLink route="/login" name="Login"/>
            </nav>
        </aside>
    );
};

export default Sidebar;