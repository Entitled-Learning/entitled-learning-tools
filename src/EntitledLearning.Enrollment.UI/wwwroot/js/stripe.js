function redirectToCheckout(sessionId) {
    const stripe = Stripe("pk_test_51QE92WQegY5BlVww3qa1DQuSIcuTKztFWZ4rKYXcs8RVbuvNtMegaxeiANS2TltZTCeGPONNrXn4xcmbzLNeEnHj00UJfpLr3Y"); // Replace with your actual publishable key
    stripe.redirectToCheckout({ sessionId: sessionId });
}
