redirectToCheckout = function (session_id) {
    var stripe = Stripe('pk_test_51JGz01SD1bow9NSl5E8n7whgMXHwr9ghY6PYBKrCjD0wNvukorLRIwIxBshiFzPCljsNX8nCVm4yAdMcZUofXFOf00uuKD6XeW');
    stripe.redirectToCheckout({
        sessionId: session_id
    });
};