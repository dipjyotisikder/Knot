import { type MetadataRoute } from "next";

export default function sitemap(): MetadataRoute.Sitemap {
    return [
        {
            url: "https://knot.yourdomain.com/",
            lastModified: new Date().toISOString(),
        },
        {
            url: "https://knot.yourdomain.com/documentation",
            lastModified: new Date().toISOString(),
        },
    ];
}
