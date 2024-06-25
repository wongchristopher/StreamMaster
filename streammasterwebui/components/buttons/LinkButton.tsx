import useCopyToClipboard from '@lib/hooks/useCopyToClipboard';
import { useState } from 'react';

export const LinkButton = ({
  link,
  filled,
  bolt,
  title
}: {
  readonly link: string;
  readonly filled?: boolean;
  readonly title: string;
  readonly bolt?: boolean;
}) => {
  const [, copyToClipboard] = useCopyToClipboard(true);
  const [copied, setCopied] = useState(false);

  const icon = bolt ? 'pi pi-bolt icon-yellow' : filled === true ? 'pi pi-bookmark-fill icon-orange' : 'pi pi-bookmark icon-orange';

  return (
    // <SMButton
    //   icon={copied ? 'pi-copy' : icon}
    //   buttonClassName={bolt ? 'icon-yellow' : 'icon-orange'}
    //   iconFilled={false}
    //   onClick={() => {
    //     Logger.debug('LinkButton', 'onClick', link.replace(/"/g, ''));
    //     copyToClipboard(link.replace(/"/g, '')).then((ifCopied) => {
    //       setCopied(ifCopied);
    //       setTimeout(() => setCopied(false), 750);
    //     });
    //   }}
    // />
    <div className="p-button-icon-only flex align-items-center justify-content-center">
      <a
        href={link}
        onClick={(e) => {
          e.preventDefault();

          void copyToClipboard(link).then((ifCopied) => {
            setCopied(ifCopied);
            setTimeout(() => setCopied(false), 750);
          });
        }}
        rel="noopener noreferrer"
        target="_blank"
        title={title}
      >
        <i className={copied ? 'pi pi-copy' : icon} />
      </a>
    </div>
  );
};
