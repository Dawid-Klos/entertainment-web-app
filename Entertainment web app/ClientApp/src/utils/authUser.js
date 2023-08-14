import axios from "axios";
export const AuthenticateUser = async () => {
    let error = false;
    
    const response = await axios
        .get('/api/Auth/AuthenticateUser')
        .catch((err) => {
            error = true;
            console.log("The problem is: ", err);
        });
    
    if (!error) {
        return response.data && response.status === 200;
    }
    
    return null;
};