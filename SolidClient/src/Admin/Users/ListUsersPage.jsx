import DashboardLayout from "../Common/DashboardLayout";

const ListUsersPage = () => (
    <DashboardLayout>
        <main class="flex flex-col px-6 pt-6 grow overflow-y-auto">
            <h1 class="mb-6 text-xl font-semibold text-gray-700">Users</h1>
            <section
                class="flex flex-col mb-6 p-4 bg-white rounded-lg shadow-md border border-gray-200">
                <h2 class="mb-4 font-semibold text-gray-800">List of Users</h2>

                <p class="text-gray-600">Placeholder for list of users</p>
            </section>
        </main>
    </DashboardLayout>
);

export default ListUsersPage;
