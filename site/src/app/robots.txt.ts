import { type MetadataRoute } from "next";

export default function robots(): MetadataRoute.Robots {
    return {
        rules: [
            {
                userAgent: '*',
                allow: '/',
            },
        ],
        sitemap: 'https://knot.yourdomain.com/sitemap.xml',
    };
}
