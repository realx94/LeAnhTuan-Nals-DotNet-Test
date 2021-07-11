const BASE_API_URL = window.location.origin;

function mapUrl(url) {
    return `${BASE_API_URL}/${url}`;
}

export {
    mapUrl
};
