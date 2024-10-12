import DefaultLayout from "./Page/DefaultLayout";

const App = () => {
    return (
        <DefaultLayout>
            <article class="flex-grow flex flex-col p-6 bg-teal-200">
                <h1 class="basis-1/12 bg-red-300"></h1>
                <div class="basis-11/12 bg-fuchsia-200"></div>
            </article>
        </DefaultLayout>
    );
};

export default App;
