// FAQ collapsible logic
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".faq-q").forEach(function (btn) {
        btn.addEventListener("click", function () {
            const item = btn.closest(".faq-item");
            const answer = item.querySelector(".faq-a");
            const expanded = item.getAttribute("aria-expanded") === "true";
            // Collapse all others
            document.querySelectorAll(".faq-item").forEach(function (it) {
                it.setAttribute("aria-expanded", "false");
                it.querySelector(".faq-q").setAttribute(
                    "aria-expanded",
                    "false"
                );
                it.querySelector(".faq-a").hidden = true;
            });
            // Expand this one if it was not already open
            if (!expanded) {
                item.setAttribute("aria-expanded", "true");
                btn.setAttribute("aria-expanded", "true");
                answer.hidden = false;
            }
        });
    });
});
// Placeholder for future JavaScript functionality for Knot marketing page.
// Add interactivity or analytics here as needed.
