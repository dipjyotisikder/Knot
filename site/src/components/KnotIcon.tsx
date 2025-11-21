import Image from "next/image";

export default function KnotIcon({
    size = 32,
    className = "",
}: {
    size?: number;
    className?: string;
}) {
    return (
        <Image
            src="/icon.jpeg"
            alt="Knot Icon"
            width={size}
            height={size}
            className={className}
            priority
        />
    );
}
