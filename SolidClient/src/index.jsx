import {render} from 'solid-js/web';

import './public/index.css';
import App from "./App";

const root = document.getElementById('root');

if (import.meta.env.DEV && !(root instanceof HTMLElement)) {
    throw new Error('Element with id "root" not found in index.html');
}

render(() => <App />, root);