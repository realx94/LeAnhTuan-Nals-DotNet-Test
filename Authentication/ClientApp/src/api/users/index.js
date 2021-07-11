import axios from "axios";
import { mapUrl } from "../../helpers/api-helper";

export default {
    create(data){
        return axios.post(mapUrl("api/users"), data);
    },
    login(data){
        return axios.post(mapUrl("api/users/login"), data);
    }
}