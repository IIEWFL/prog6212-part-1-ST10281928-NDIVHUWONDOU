// ====== Theme Toggle with persistence ======
(function () {
    const root = document.documentElement;
    const saved = localStorage.getItem("theme");
    if (saved === "dark" || saved === "light") {
        root.setAttribute("data-theme", saved);
    } else {
        // default light; change to 'dark' to start in dark
        root.setAttribute("data-theme", "light");
    }
    const btn = document.getElementById("themeToggle");
    if (btn) {
        const setLabel = () => {
            const current = root.getAttribute("data-theme");
            btn.innerHTML = (current === "dark")
                ? '<i class="bi bi-brightness-high me-1"></i><span>Light</span>'
                : '<i class="bi bi-moon-stars me-1"></i><span>Dark</span>';
        };
        setLabel();
        btn.addEventListener("click", () => {
            const current = root.getAttribute("data-theme");
            const next = current === "dark" ? "light" : "dark";
            root.setAttribute("data-theme", next);
            localStorage.setItem("theme", next);
            setLabel();
        });
    }
})();

// ====== Active Nav Link based on current path ======
(function () {
    const path = window.location.pathname.toLowerCase();
    document.querySelectorAll(".navbar a.nav-link").forEach(a => {
        const href = a.getAttribute("href") || "";
        if (!href) return;
        if (path === "/" && href === "/") {
            a.classList.add("active");
        } else if (href !== "/" && path.indexOf(href.toLowerCase()) === 0) {
            a.classList.add("active");
        }
    });
})();
