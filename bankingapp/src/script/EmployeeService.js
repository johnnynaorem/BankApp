import axios from "axios";

export const getAllCustomers = () => {
  try {
    const response = axios.get("http://localhost:5194/api/Customer");
    return response;
  } catch (error) {
    alert("Error while fetching customers");
    console.error(error);
  }
};

export const getCustomerById = (id) => {
  try {
    const response = axios.get(`http://localhost:5194/api/Customer/${id}`);
    return response;
  } catch (error) {
    alert("Error while fetching customers");
    console.error(error);
  }
};

export const createCustomer = (
  firstName,
  lastName,
  phone,
  email,
  address,
  city,
  dob
) => {
  try {
    const response = axios.post(`http://localhost:5194/api/Customer`, {
      firstName: firstName,
      lastName: lastName,
      phone: phone,
      address: address,
      email: email,
      city: city,
      dob: dob,
    });
    return response;
  } catch (error) {
    alert("Error while Creating customers");
    console.error(error);
  }
};

export const createCustomerApiForGoLang = (
  firstName,
  lastName,
  phone,
  email,
  address,
  city,
  dob
) => {
  try {
    const response = axios.post(`http://localhost:8080/add-customer`, {
      firstName: firstName,
      lastName: lastName,
      phone: phone,
      address: address,
      email: email,
      city: city,
      DateOfBirth: dob,
    });
    return response;
  } catch (error) {
    alert("Error while Creating customers");
    console.error(error);
  }
};

export const getAllCustomersApiForGoLang = () => {
  try {
    const response = axios.get("http://localhost:8080/get-customers");
    return response;
  } catch (error) {
    alert("Error while fetching customers");
    console.error(error);
  }
};

export const updateCustomerAPI = (
  id,
  firstName,
  lastName,
  phone,
  email,
  address,
  city
) => {
  try {
    const response = axios.patch(`http://localhost:5194/api/Customer/${id}`, {
      firstName: firstName,
      lastName: lastName,
      phone: phone,
      address: address,
      email: email,
      city: city,
    });
    return response;
  } catch (error) {
    alert("Error while Updating customers");
    console.error(error);
  }
};

export const deleteCustomer = (id) => {
  try {
    const response = axios.delete(
      `http://localhost:5194/api/Customer?id=${id}`
    );
    return response;
  } catch (error) {
    alert("Error while deleting customer");
    console.error(error);
  }
};
