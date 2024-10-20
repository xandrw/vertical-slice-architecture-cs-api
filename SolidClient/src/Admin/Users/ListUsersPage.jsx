import DashboardLayout from "../Common/DashboardLayout";
import {auth} from "../../Store/auth";
import {useNavigate} from "@solidjs/router";
import Table from "../../Components/Table/Table";
import api from "../../api";
import {createResource, createSignal} from "solid-js";

const ListUsersPage = () => {
    if (!auth.isAdmin) {
        const navigate = useNavigate();
        navigate('/login');
        return null;
    }

    console.log(auth.user);

    const [pageNumber, setPageNumber] = createSignal(1);
    const listUsers = async ({pageNumber, pageSize}) => await api.listUsers({pageNumber, pageSize});

    const [paginatedUsers] = createResource(
        () => ({pageNumber: pageNumber(), pageSize: 10}),
        listUsers
    );
    const {items, totalCount, pageSize} = paginatedUsers() || {items: [], totalCount: 0, pageSize: 10};

    return (
        <DashboardLayout>
            <main class="flex flex-col px-6 pt-6 grow overflow-y-auto">
                <h1 class="mb-6 text-xl font-semibold text-gray-700">Users</h1>

                <div
                    class="w-full lg:w-1/2  mb-6 overflow-y-hidden overflow-x-auto border border-gray-200 rounded-lg shadow-md">
                    <Table items={items}
                           pageNumber={pageNumber()}
                           pageSize={pageSize}
                           totalCount={totalCount}
                           setPageNumber={setPageNumber}
                    />
                </div>
            </main>
        </DashboardLayout>
    );
}

export default ListUsersPage;
