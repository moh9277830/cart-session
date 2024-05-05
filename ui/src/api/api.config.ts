import axios from "axios";

let baseURL = `${import.meta.env.VITE_BASE_URL}/api`;

const api = axios.create({
    baseURL,
    headers: {
        "Content-Type": "application/json",
        "Accept": "application/json",
        "X-Requested-With": "XMLHttpRequest"
    }
});

export default api;