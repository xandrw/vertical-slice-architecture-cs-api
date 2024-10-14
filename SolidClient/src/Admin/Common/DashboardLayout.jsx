import Sidebar from "./Sidebar";

const AdminLayout = ({children}) => (
    <div class="flex h-screen bg-gray-50">
        <Sidebar/>
        {children}
    </div>
);

export default AdminLayout;