import {For} from "solid-js";

const Table = ({items = [], totalCount, pageNumber, pageSize, setPageNumber}) => {
    const headerColumns = items.some(item => item !== undefined) ? Object.keys(items[0]) : [];
    const totalPages = Math.ceil(totalCount / pageSize);

    console.log({items, totalCount, pageNumber, pageSize});

    return (
        <>
            <table class="w-full whitespace-nowrap">
                <thead class="border-b border-gray-200">
                <tr class="text-xs font-semibold tracking-wide text-left text-gray-500 uppercase bg-gray-50">
                    <For each={headerColumns}>
                        {headerColumn => (
                            <th class="px-4 py-3">{headerColumn}</th>
                        )}
                    </For>
                    <th class="px-4 py-3">Actions</th>
                </tr>
                </thead>
                <tbody class="bg-white divide-y">
                <For each={items}>
                    {item => (
                        <tr>
                            <For each={headerColumns}>
                                {headerColumn => (
                                    <td class="px-4 py-3">{item[headerColumn]}</td>
                                )}
                            </For>
                            <td class="px-4 py-3">[Edit] [Delete]</td>
                        </tr>
                    )}
                </For>
                </tbody>
            </table>

            <div
                class="grid px-4 py-3 text-xs font-semibold tracking-wide text-gray-500 uppercase border-t bg-gray sm:grid-cols-9">
                <span class="flex items-center col-span-3">
                    Showing page {pageNumber} of {totalPages}
                </span>
                <nav class="inline-flex items-center col-span-6 sm:justify-end">
                    <button class="px-3 py-1 rounded-md rounded-l-lg focus:outline-none focus:shadow-outline-purple"
                            disabled={pageNumber === 1}
                            on:click={() => setPageNumber(pageNumber - 1)}>
                        &lt;
                    </button>
                    <button class="px-3 py-1 rounded-md rounded-r-lg focus:outline-none focus:shadow-outline-purple"
                            disabled={pageNumber === totalPages}
                            on:click={() => setPageNumber(pageNumber + 1)}>
                        &gt;
                    </button>
                </nav>
            </div>
        </>
    );
};

export default Table;